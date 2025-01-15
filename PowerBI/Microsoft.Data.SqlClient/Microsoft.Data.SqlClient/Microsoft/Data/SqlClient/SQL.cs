using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Transactions;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Data.SqlTypes;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000F6 RID: 246
	internal static class SQL
	{
		// Token: 0x060012F4 RID: 4852 RVA: 0x0004CA45 File Offset: 0x0004AC45
		internal static Exception CannotGetDTCAddress()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_CannotGetDTCAddress, Array.Empty<object>()));
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0004CA5B File Offset: 0x0004AC5B
		internal static Exception InvalidOptionLength(string key)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_InvalidOptionLength, new object[] { key }));
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0004CA76 File Offset: 0x0004AC76
		internal static Exception InvalidInternalPacketSize(string str)
		{
			return ADP.ArgumentOutOfRange(str);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0004CA7E File Offset: 0x0004AC7E
		internal static Exception InvalidPacketSize()
		{
			return ADP.ArgumentOutOfRange(StringsHelper.GetString(Strings.SQL_InvalidTDSPacketSize, Array.Empty<object>()));
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0004CA94 File Offset: 0x0004AC94
		internal static Exception InvalidPacketSizeValue()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_InvalidPacketSizeValue, Array.Empty<object>()));
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0004CAAA File Offset: 0x0004ACAA
		internal static Exception InvalidSSPIPacketSize()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_InvalidSSPIPacketSize, Array.Empty<object>()));
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0004CAC0 File Offset: 0x0004ACC0
		internal static Exception NullEmptyTransactionName()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_NullEmptyTransactionName, Array.Empty<object>()));
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004CAD6 File Offset: 0x0004ACD6
		internal static Exception SnapshotNotSupported(global::System.Data.IsolationLevel level)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_SnapshotNotSupported, new object[]
			{
				typeof(global::System.Data.IsolationLevel),
				level.ToString()
			}));
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0004CB0A File Offset: 0x0004AD0A
		internal static Exception UserInstanceFailoverNotCompatible()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_UserInstanceFailoverNotCompatible, Array.Empty<object>()));
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004CB20 File Offset: 0x0004AD20
		internal static Exception CredentialsNotProvided(SqlAuthenticationMethod auth)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_CredentialsNotProvided, new object[] { DbConnectionStringBuilderUtil.AuthenticationTypeToString(auth) }));
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0004CB40 File Offset: 0x0004AD40
		internal static Exception InvalidCertAuth()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_Certificate, Array.Empty<object>()));
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004CB56 File Offset: 0x0004AD56
		internal static Exception AuthenticationAndIntegratedSecurity()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_AuthenticationAndIntegratedSecurity, Array.Empty<object>()));
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0004CB6C File Offset: 0x0004AD6C
		internal static Exception IntegratedWithPassword()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_IntegratedWithPassword, Array.Empty<object>()));
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004CB82 File Offset: 0x0004AD82
		internal static Exception InteractiveWithPassword()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_InteractiveWithPassword, Array.Empty<object>()));
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0004CB98 File Offset: 0x0004AD98
		internal static Exception DeviceFlowWithUsernamePassword()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_DeviceFlowWithUsernamePassword, Array.Empty<object>()));
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0004CBAE File Offset: 0x0004ADAE
		internal static Exception NonInteractiveWithPassword(string authenticationMode)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_NonInteractiveWithPassword, new object[] { authenticationMode }));
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0004CBC9 File Offset: 0x0004ADC9
		internal static Exception SettingIntegratedWithCredential()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SettingIntegratedWithCredential, Array.Empty<object>()));
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0004CBDF File Offset: 0x0004ADDF
		internal static Exception SettingInteractiveWithCredential()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SettingInteractiveWithCredential, Array.Empty<object>()));
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0004CBF5 File Offset: 0x0004ADF5
		internal static Exception SettingDeviceFlowWithCredential()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SettingDeviceFlowWithCredential, Array.Empty<object>()));
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0004CC0B File Offset: 0x0004AE0B
		internal static Exception SettingNonInteractiveWithCredential(string authenticationMode)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SettingNonInteractiveWithCredential, new object[] { authenticationMode }));
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0004CC26 File Offset: 0x0004AE26
		internal static Exception SettingCredentialWithIntegratedArgument()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_SettingCredentialWithIntegrated, Array.Empty<object>()));
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0004CC3C File Offset: 0x0004AE3C
		internal static Exception SettingCredentialWithInteractiveArgument()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_SettingCredentialWithInteractive, Array.Empty<object>()));
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0004CC52 File Offset: 0x0004AE52
		internal static Exception SettingCredentialWithDeviceFlowArgument()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_SettingCredentialWithDeviceFlow, Array.Empty<object>()));
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0004CC68 File Offset: 0x0004AE68
		internal static Exception SettingCredentialWithNonInteractiveArgument(string authenticationMode)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_SettingCredentialWithNonInteractive, new object[] { authenticationMode }));
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0004CC83 File Offset: 0x0004AE83
		internal static Exception SettingCredentialWithIntegratedInvalid()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SettingCredentialWithIntegrated, Array.Empty<object>()));
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0004CC99 File Offset: 0x0004AE99
		internal static Exception SettingCredentialWithInteractiveInvalid()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SettingCredentialWithInteractive, Array.Empty<object>()));
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0004CCAF File Offset: 0x0004AEAF
		internal static Exception SettingCredentialWithDeviceFlowInvalid()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SettingCredentialWithDeviceFlow, Array.Empty<object>()));
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0004CCC5 File Offset: 0x0004AEC5
		internal static Exception SettingCredentialWithNonInteractiveInvalid(string authenticationMode)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SettingCredentialWithNonInteractive, new object[] { authenticationMode }));
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0004CCE0 File Offset: 0x0004AEE0
		internal static Exception InvalidSQLServerVersionUnknown()
		{
			return ADP.DataAdapter(StringsHelper.GetString(Strings.SQL_InvalidSQLServerVersionUnknown, Array.Empty<object>()));
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0004CCF6 File Offset: 0x0004AEF6
		internal static Exception SynchronousCallMayNotPend()
		{
			return new Exception(StringsHelper.GetString(Strings.Sql_InternalError, Array.Empty<object>()));
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0004CD0C File Offset: 0x0004AF0C
		internal static Exception ConnectionLockedForBcpEvent()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ConnectionLockedForBcpEvent, Array.Empty<object>()));
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004CD22 File Offset: 0x0004AF22
		internal static Exception FatalTimeout()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_FatalTimeout, Array.Empty<object>()));
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004CD38 File Offset: 0x0004AF38
		internal static Exception InstanceFailure()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_InstanceFailure, Array.Empty<object>()));
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004CD4E File Offset: 0x0004AF4E
		internal static Exception ChangePasswordArgumentMissing(string argumentName)
		{
			return ADP.ArgumentNull(StringsHelper.GetString(Strings.SQL_ChangePasswordArgumentMissing, new object[] { argumentName }));
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0004CD69 File Offset: 0x0004AF69
		internal static Exception ChangePasswordConflictsWithSSPI()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_ChangePasswordConflictsWithSSPI, Array.Empty<object>()));
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0004CD7F File Offset: 0x0004AF7F
		internal static Exception ChangePasswordRequires2005()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ChangePasswordRequiresYukon, Array.Empty<object>()));
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0004CD95 File Offset: 0x0004AF95
		internal static Exception UnknownSysTxIsolationLevel(global::System.Transactions.IsolationLevel isolationLevel)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_UnknownSysTxIsolationLevel, new object[] { isolationLevel.ToString() }));
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0004CDBC File Offset: 0x0004AFBC
		internal static Exception ChangePasswordUseOfUnallowedKey(string key)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ChangePasswordUseOfUnallowedKey, new object[] { key }));
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0004CDD7 File Offset: 0x0004AFD7
		internal static Exception InvalidPartnerConfiguration(string server, string database)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_InvalidPartnerConfiguration, new object[] { server, database }));
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0004CDF6 File Offset: 0x0004AFF6
		internal static Exception BatchedUpdateColumnEncryptionSettingMismatch()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_BatchedUpdateColumnEncryptionSettingMismatch, new object[] { "SqlCommandColumnEncryptionSetting", "SelectCommand", "InsertCommand", "UpdateCommand", "DeleteCommand" }));
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0004CE35 File Offset: 0x0004B035
		internal static Exception MARSUnsupportedOnConnection()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_MarsUnsupportedOnConnection, Array.Empty<object>()));
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0004CE4B File Offset: 0x0004B04B
		internal static Exception CannotModifyPropertyAsyncOperationInProgress(string property)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_CannotModifyPropertyAsyncOperationInProgress, new object[] { property }));
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0004CE66 File Offset: 0x0004B066
		internal static Exception NonLocalSSEInstance()
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SQL_NonLocalSSEInstance, Array.Empty<object>()));
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0004CE7C File Offset: 0x0004B07C
		internal static Exception UnsupportedAuthentication(string authentication)
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SQL_UnsupportedAuthentication, new object[] { authentication }));
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0004CE97 File Offset: 0x0004B097
		internal static Exception UnsupportedSqlAuthenticationMethod(SqlAuthenticationMethod authentication)
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SQL_UnsupportedSqlAuthenticationMethod, new object[] { authentication }));
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0004CEB7 File Offset: 0x0004B0B7
		internal static Exception UnsupportedAuthenticationSpecified(SqlAuthenticationMethod authentication)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_UnsupportedAuthenticationSpecified, new object[] { authentication }));
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0004CED7 File Offset: 0x0004B0D7
		internal static Exception CannotCreateAuthProvider(string authentication, string type, Exception e)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_CannotCreateAuthProvider, new object[] { authentication, type }), e);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0004CEF7 File Offset: 0x0004B0F7
		internal static Exception CannotCreateSqlAuthInitializer(string type, Exception e)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_CannotCreateAuthInitializer, new object[] { type }), e);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004CF13 File Offset: 0x0004B113
		internal static Exception CannotInitializeAuthProvider(string type, Exception e)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_CannotInitializeAuthProvider, new object[] { type }), e);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004CF2F File Offset: 0x0004B12F
		internal static Exception UnsupportedAuthenticationByProvider(string authentication, string type)
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SQL_UnsupportedAuthenticationByProvider, new object[] { type, authentication }));
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004CF4E File Offset: 0x0004B14E
		internal static Exception CannotFindAuthProvider(string authentication)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_CannotFindAuthProvider, new object[] { authentication }));
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0004CF69 File Offset: 0x0004B169
		internal static Exception CannotGetAuthProviderConfig(Exception e)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_CannotGetAuthProviderConfig, Array.Empty<object>()), e);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0004CF80 File Offset: 0x0004B180
		internal static Exception ParameterCannotBeEmpty(string paramName)
		{
			return ADP.ArgumentNull(StringsHelper.GetString(Strings.SQL_ParameterCannotBeEmpty, new object[] { paramName }));
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004CF9B File Offset: 0x0004B19B
		internal static Exception ParameterDirectionInvalidForOptimizedBinding(string paramName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ParameterDirectionInvalidForOptimizedBinding, new object[] { paramName }));
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004CFB6 File Offset: 0x0004B1B6
		internal static Exception ActiveDirectoryInteractiveTimeout()
		{
			return ADP.TimeoutException(Strings.SQL_Timeout_Active_Directory_Interactive_Authentication, null);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004CFC3 File Offset: 0x0004B1C3
		internal static Exception ActiveDirectoryDeviceFlowTimeout()
		{
			return ADP.TimeoutException(Strings.SQL_Timeout_Active_Directory_DeviceFlow_Authentication, null);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0004CFD0 File Offset: 0x0004B1D0
		internal static Exception ActiveDirectoryTokenRetrievingTimeout(string authenticaton, string errorCode, Exception exception)
		{
			return ADP.TimeoutException(StringsHelper.GetString(Strings.AAD_Token_Retrieving_Timeout, new object[]
			{
				authenticaton,
				errorCode,
				(exception != null) ? exception.Message : null
			}), exception);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004CFFF File Offset: 0x0004B1FF
		internal static Exception NotificationsRequire2005()
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SQL_NotificationsRequireYukon, Array.Empty<object>()));
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004D015 File Offset: 0x0004B215
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(StringsHelper.GetString(Strings.SQL_NotSupportedEnumerationValue, new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004D04A File Offset: 0x0004B24A
		internal static ArgumentOutOfRangeException NotSupportedCommandType(CommandType value)
		{
			return SQL.NotSupportedEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0004D05C File Offset: 0x0004B25C
		internal static ArgumentOutOfRangeException NotSupportedIsolationLevel(global::System.Data.IsolationLevel value)
		{
			return SQL.NotSupportedEnumerationValue(typeof(global::System.Data.IsolationLevel), (int)value);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0004D070 File Offset: 0x0004B270
		internal static Exception OperationCancelled()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_OperationCancelled, Array.Empty<object>()));
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0004D093 File Offset: 0x0004B293
		internal static Exception PendingBeginXXXExists()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_PendingBeginXXXExists, Array.Empty<object>()));
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0004D0A9 File Offset: 0x0004B2A9
		internal static ArgumentOutOfRangeException InvalidSqlDependencyTimeout(string param)
		{
			return ADP.ArgumentOutOfRange(StringsHelper.GetString(Strings.SqlDependency_InvalidTimeout, Array.Empty<object>()), param);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0004D0C0 File Offset: 0x0004B2C0
		internal static Exception NonXmlResult()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_NonXmlResult, Array.Empty<object>()));
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004D0D6 File Offset: 0x0004B2D6
		internal static Exception InvalidUdt3PartNameFormat()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_InvalidUdt3PartNameFormat, Array.Empty<object>()));
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004D0EC File Offset: 0x0004B2EC
		internal static Exception InvalidParameterTypeNameFormat()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_InvalidParameterTypeNameFormat, Array.Empty<object>()));
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004D102 File Offset: 0x0004B302
		internal static Exception InvalidParameterNameLength(string value)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_InvalidParameterNameLength, new object[] { value }));
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0004D11D File Offset: 0x0004B31D
		internal static Exception PrecisionValueOutOfRange(byte precision)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_PrecisionValueOutOfRange, new object[] { precision.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0004D143 File Offset: 0x0004B343
		internal static Exception ScaleValueOutOfRange(byte scale)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_ScaleValueOutOfRange, new object[] { scale.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004D169 File Offset: 0x0004B369
		internal static Exception TimeScaleValueOutOfRange(byte scale)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_TimeScaleValueOutOfRange, new object[] { scale.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0004D18F File Offset: 0x0004B38F
		internal static Exception InvalidSqlDbType(SqlDbType value)
		{
			return ADP.InvalidEnumerationValue(typeof(SqlDbType), (int)value);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0004D1A1 File Offset: 0x0004B3A1
		internal static Exception UnsupportedTVPOutputParameter(ParameterDirection direction, string paramName)
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SqlParameter_UnsupportedTVPOutputParameter, new object[]
			{
				direction.ToString(),
				paramName
			}));
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0004D1CC File Offset: 0x0004B3CC
		internal static Exception DBNullNotSupportedForTVPValues(string paramName)
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SqlParameter_DBNullNotSupportedForTVP, new object[] { paramName }));
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0004D1E7 File Offset: 0x0004B3E7
		internal static Exception InvalidTableDerivedPrecisionForTvp(string columnName, byte precision)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlParameter_InvalidTableDerivedPrecisionForTvp, new object[]
			{
				precision,
				columnName,
				SqlDecimal.MaxPrecision
			}));
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0004D218 File Offset: 0x0004B418
		internal static Exception UnexpectedTypeNameForNonStructParams(string paramName)
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SqlParameter_UnexpectedTypeNameForNonStruct, new object[] { paramName }));
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0004D233 File Offset: 0x0004B433
		internal static Exception SingleValuedStructNotSupported()
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.MetaType_SingleValuedStructNotSupported, Array.Empty<object>()));
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0004D249 File Offset: 0x0004B449
		internal static Exception ParameterInvalidVariant(string paramName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ParameterInvalidVariant, new object[] { paramName }));
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0004D264 File Offset: 0x0004B464
		internal static Exception MustSetTypeNameForParam(string paramType, string paramName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_ParameterTypeNameRequired, new object[] { paramType, paramName }));
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0004D283 File Offset: 0x0004B483
		internal static Exception NullSchemaTableDataTypeNotSupported(string columnName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.NullSchemaTableDataTypeNotSupported, new object[] { columnName }));
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0004D29E File Offset: 0x0004B49E
		internal static Exception InvalidSchemaTableOrdinals()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.InvalidSchemaTableOrdinals, Array.Empty<object>()));
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0004D2B4 File Offset: 0x0004B4B4
		internal static Exception EnumeratedRecordMetaDataChanged(string fieldName, int recordNumber)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_EnumeratedRecordMetaDataChanged, new object[] { fieldName, recordNumber }));
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0004D2D8 File Offset: 0x0004B4D8
		internal static Exception EnumeratedRecordFieldCountChanged(int recordNumber)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_EnumeratedRecordFieldCountChanged, new object[] { recordNumber }));
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0004D2F8 File Offset: 0x0004B4F8
		internal static Exception InvalidTDSVersion()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_InvalidTDSVersion, Array.Empty<object>()));
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0004D310 File Offset: 0x0004B510
		internal static Exception ParsingError(ParsingErrorState state)
		{
			string sql_ParsingErrorWithState = Strings.SQL_ParsingErrorWithState;
			object[] array = new object[1];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			return ADP.InvalidOperation(StringsHelper.GetString(sql_ParsingErrorWithState, array));
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0004D344 File Offset: 0x0004B544
		internal static Exception ParsingError(ParsingErrorState state, Exception innerException)
		{
			string sql_ParsingErrorWithState = Strings.SQL_ParsingErrorWithState;
			object[] array = new object[1];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			return ADP.InvalidOperation(StringsHelper.GetString(sql_ParsingErrorWithState, array), innerException);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0004D378 File Offset: 0x0004B578
		internal static Exception ParsingErrorValue(ParsingErrorState state, int value)
		{
			string sql_ParsingErrorValue = Strings.SQL_ParsingErrorValue;
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = value;
			return ADP.InvalidOperation(StringsHelper.GetString(sql_ParsingErrorValue, array));
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0004D3B4 File Offset: 0x0004B5B4
		internal static Exception ParsingErrorOffset(ParsingErrorState state, int offset)
		{
			string sql_ParsingErrorOffset = Strings.SQL_ParsingErrorOffset;
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = offset;
			return ADP.InvalidOperation(StringsHelper.GetString(sql_ParsingErrorOffset, array));
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0004D3F0 File Offset: 0x0004B5F0
		internal static Exception ParsingErrorFeatureId(ParsingErrorState state, int featureId)
		{
			string sql_ParsingErrorFeatureId = Strings.SQL_ParsingErrorFeatureId;
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = featureId;
			return ADP.InvalidOperation(StringsHelper.GetString(sql_ParsingErrorFeatureId, array));
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0004D42C File Offset: 0x0004B62C
		internal static Exception ParsingErrorToken(ParsingErrorState state, int token)
		{
			string sql_ParsingErrorToken = Strings.SQL_ParsingErrorToken;
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = token;
			return ADP.InvalidOperation(StringsHelper.GetString(sql_ParsingErrorToken, array));
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0004D468 File Offset: 0x0004B668
		internal static Exception ParsingErrorLength(ParsingErrorState state, int length)
		{
			string sql_ParsingErrorLength = Strings.SQL_ParsingErrorLength;
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = length;
			return ADP.InvalidOperation(StringsHelper.GetString(sql_ParsingErrorLength, array));
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0004D4A4 File Offset: 0x0004B6A4
		internal static Exception ParsingErrorStatus(ParsingErrorState state, int status)
		{
			string sql_ParsingErrorStatus = Strings.SQL_ParsingErrorStatus;
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = status;
			return ADP.InvalidOperation(StringsHelper.GetString(sql_ParsingErrorStatus, array));
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0004D4E0 File Offset: 0x0004B6E0
		internal static Exception ParsingErrorLibraryType(ParsingErrorState state, int libraryType)
		{
			string sql_ParsingErrorAuthLibraryType = Strings.SQL_ParsingErrorAuthLibraryType;
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = libraryType;
			return ADP.InvalidOperation(StringsHelper.GetString(sql_ParsingErrorAuthLibraryType, array));
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0004D51C File Offset: 0x0004B71C
		internal static Exception MoneyOverflow(string moneyValue)
		{
			return ADP.Overflow(StringsHelper.GetString(Strings.SQL_MoneyOverflow, new object[] { moneyValue }));
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0004D537 File Offset: 0x0004B737
		internal static Exception SmallDateTimeOverflow(string datetime)
		{
			return ADP.Overflow(StringsHelper.GetString(Strings.SQL_SmallDateTimeOverflow, new object[] { datetime }));
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0004D552 File Offset: 0x0004B752
		internal static Exception SNIPacketAllocationFailure()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SNIPacketAllocationFailure, Array.Empty<object>()));
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0004D568 File Offset: 0x0004B768
		internal static Exception TimeOverflow(string time)
		{
			return ADP.Overflow(StringsHelper.GetString(Strings.SQL_TimeOverflow, new object[] { time }));
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0004D583 File Offset: 0x0004B783
		internal static Exception InvalidServerCertificate()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_InvalidServerCertificate, Array.Empty<object>()));
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0004D599 File Offset: 0x0004B799
		internal static Exception InvalidRead()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_InvalidRead, Array.Empty<object>()));
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0004D5AF File Offset: 0x0004B7AF
		internal static Exception NonBlobColumn(string columnName)
		{
			return ADP.InvalidCast(StringsHelper.GetString(Strings.SQL_NonBlobColumn, new object[] { columnName }));
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004D5CA File Offset: 0x0004B7CA
		internal static Exception NonCharColumn(string columnName)
		{
			return ADP.InvalidCast(StringsHelper.GetString(Strings.SQL_NonCharColumn, new object[] { columnName }));
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004D5E5 File Offset: 0x0004B7E5
		internal static Exception StreamNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(StringsHelper.GetString(Strings.SQL_StreamNotSupportOnColumnType, new object[] { columnName }));
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0004D600 File Offset: 0x0004B800
		internal static Exception StreamNotSupportOnEncryptedColumn(string columnName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_StreamNotSupportOnEncryptedColumn, new object[] { columnName, "Stream" }));
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004D623 File Offset: 0x0004B823
		internal static Exception SequentialAccessNotSupportedOnEncryptedColumn(string columnName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_SequentialAccessNotSupportedOnEncryptedColumn, new object[] { columnName, "CommandBehavior=SequentialAccess" }));
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004D646 File Offset: 0x0004B846
		internal static Exception TextReaderNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(StringsHelper.GetString(Strings.SQL_TextReaderNotSupportOnColumnType, new object[] { columnName }));
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0004D661 File Offset: 0x0004B861
		internal static Exception XmlReaderNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(StringsHelper.GetString(Strings.SQL_XmlReaderNotSupportOnColumnType, new object[] { columnName }));
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0004D67C File Offset: 0x0004B87C
		internal static Exception UDTUnexpectedResult(string exceptionText)
		{
			return ADP.TypeLoad(StringsHelper.GetString(Strings.SQLUDT_Unexpected, new object[] { exceptionText }));
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0004D698 File Offset: 0x0004B898
		internal static Exception CannotCompleteDelegatedTransactionWithOpenResults(SqlInternalConnectionTds internalConnection, bool marsOn)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(-2, 0, 11, null, StringsHelper.GetString(Strings.ADP_OpenReaderExists, new object[] { marsOn ? "Command" : "Connection" }), "", 0, 258U, null)
			}, null, internalConnection, null);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0004D6F4 File Offset: 0x0004B8F4
		internal static TransactionPromotionException PromotionFailed(Exception inner)
		{
			TransactionPromotionException ex = new TransactionPromotionException(StringsHelper.GetString(Strings.SqlDelegatedTransaction_PromotionFailed, Array.Empty<object>()), inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0004D71E File Offset: 0x0004B91E
		internal static Exception DateTimeOverflow()
		{
			return new OverflowException(SQLResource.DateTimeOverflowMessage);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0004D72A File Offset: 0x0004B92A
		internal static Exception SqlCommandHasExistingSqlNotificationRequest()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQLNotify_AlreadyHasCommand, Array.Empty<object>()));
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0004D740 File Offset: 0x0004B940
		internal static Exception SqlDepCannotBeCreatedInProc()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlNotify_SqlDepCannotBeCreatedInProc, Array.Empty<object>()));
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0004D756 File Offset: 0x0004B956
		internal static Exception SqlDepDefaultOptionsButNoStart()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlDependency_DefaultOptionsButNoStart, Array.Empty<object>()));
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0004D76C File Offset: 0x0004B96C
		internal static Exception SqlDependencyDatabaseBrokerDisabled()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlDependency_DatabaseBrokerDisabled, Array.Empty<object>()));
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0004D782 File Offset: 0x0004B982
		internal static Exception SqlDependencyEventNoDuplicate()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlDependency_EventNoDuplicate, Array.Empty<object>()));
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0004D798 File Offset: 0x0004B998
		internal static Exception SqlDependencyDuplicateStart()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlDependency_DuplicateStart, Array.Empty<object>()));
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0004D7AE File Offset: 0x0004B9AE
		internal static Exception SqlDependencyIdMismatch()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlDependency_IdMismatch, Array.Empty<object>()));
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0004D7C4 File Offset: 0x0004B9C4
		internal static Exception SqlDependencyNoMatchingServerStart()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlDependency_NoMatchingServerStart, Array.Empty<object>()));
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0004D7DA File Offset: 0x0004B9DA
		internal static Exception SqlDependencyNoMatchingServerDatabaseStart()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlDependency_NoMatchingServerDatabaseStart, Array.Empty<object>()));
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0004D7F0 File Offset: 0x0004B9F0
		internal static Exception SqlNotificationException(SqlNotificationEventArgs notify)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQLNotify_ErrorFormat, new object[] { notify.Type, notify.Info, notify.Source }));
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0004D83C File Offset: 0x0004BA3C
		internal static Exception SqlMetaDataNoMetaData()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlMetaData_NoMetadata, Array.Empty<object>()));
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0004D852 File Offset: 0x0004BA52
		internal static Exception MustSetUdtTypeNameForUdtParams()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQLUDT_InvalidUdtTypeName, Array.Empty<object>()));
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0004D868 File Offset: 0x0004BA68
		internal static Exception UnexpectedUdtTypeNameForNonUdtParams()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQLUDT_UnexpectedUdtTypeName, Array.Empty<object>()));
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0004D87E File Offset: 0x0004BA7E
		internal static Exception UDTInvalidSqlType(string typeName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQLUDT_InvalidSqlType, new object[] { typeName }));
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004D899 File Offset: 0x0004BA99
		internal static Exception UDTInvalidSize(int maxSize, int maxSupportedSize)
		{
			throw ADP.ArgumentOutOfRange(StringsHelper.GetString(Strings.SQLUDT_InvalidSize, new object[] { maxSize, maxSupportedSize }));
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0004D8C2 File Offset: 0x0004BAC2
		internal static Exception InvalidSqlDbTypeForConstructor(SqlDbType type)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SqlMetaData_InvalidSqlDbTypeForConstructorFormat, new object[] { type.ToString() }));
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0004D8E9 File Offset: 0x0004BAE9
		internal static Exception NameTooLong(string parameterName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SqlMetaData_NameTooLong, Array.Empty<object>()), parameterName);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0004D900 File Offset: 0x0004BB00
		internal static Exception InvalidSortOrder(SortOrder order)
		{
			return ADP.InvalidEnumerationValue(typeof(SortOrder), (int)order);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0004D912 File Offset: 0x0004BB12
		internal static Exception MustSpecifyBothSortOrderAndOrdinal(SortOrder order, int ordinal)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlMetaData_SpecifyBothSortOrderAndOrdinal, new object[]
			{
				order.ToString(),
				ordinal
			}));
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0004D942 File Offset: 0x0004BB42
		internal static Exception TableTypeCanOnlyBeParameter()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQLTVP_TableTypeCanOnlyBeParameter, Array.Empty<object>()));
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004D958 File Offset: 0x0004BB58
		internal static Exception UnsupportedColumnTypeForSqlProvider(string columnName, string typeName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SqlProvider_InvalidDataColumnType, new object[] { columnName, typeName }));
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0004D977 File Offset: 0x0004BB77
		internal static Exception InvalidColumnMaxLength(string columnName, long maxLength)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SqlProvider_InvalidDataColumnMaxLength, new object[] { columnName, maxLength }));
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004D99B File Offset: 0x0004BB9B
		internal static Exception InvalidColumnPrecScale()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SqlMisc_InvalidPrecScaleMessage, Array.Empty<object>()));
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0004D9B1 File Offset: 0x0004BBB1
		internal static Exception NotEnoughColumnsInStructuredType()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SqlProvider_NotEnoughColumnsInStructuredType, Array.Empty<object>()));
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0004D9C7 File Offset: 0x0004BBC7
		internal static Exception DuplicateSortOrdinal(int sortOrdinal)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlProvider_DuplicateSortOrdinal, new object[] { sortOrdinal }));
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0004D9E7 File Offset: 0x0004BBE7
		internal static Exception MissingSortOrdinal(int sortOrdinal)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlProvider_MissingSortOrdinal, new object[] { sortOrdinal }));
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0004DA07 File Offset: 0x0004BC07
		internal static Exception SortOrdinalGreaterThanFieldCount(int columnOrdinal, int sortOrdinal)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlProvider_SortOrdinalGreaterThanFieldCount, new object[] { sortOrdinal, columnOrdinal }));
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0004DA30 File Offset: 0x0004BC30
		internal static Exception IEnumerableOfSqlDataRecordHasNoRows()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.IEnumerableOfSqlDataRecordHasNoRows, Array.Empty<object>()));
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0004DA46 File Offset: 0x0004BC46
		internal static Exception SqlPipeCommandHookedUpToNonContextConnection()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlPipe_CommandHookedUpToNonContextConnection, Array.Empty<object>()));
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0004DA5C File Offset: 0x0004BC5C
		internal static Exception SqlPipeMessageTooLong(int messageLength)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SqlPipe_MessageTooLong, new object[] { messageLength }));
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004DA7C File Offset: 0x0004BC7C
		internal static Exception SqlPipeIsBusy()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlPipe_IsBusy, Array.Empty<object>()));
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0004DA92 File Offset: 0x0004BC92
		internal static Exception SqlPipeAlreadyHasAnOpenResultSet(string methodName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlPipe_AlreadyHasAnOpenResultSet, new object[] { methodName }));
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0004DAAD File Offset: 0x0004BCAD
		internal static Exception SqlPipeDoesNotHaveAnOpenResultSet(string methodName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlPipe_DoesNotHaveAnOpenResultSet, new object[] { methodName }));
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0004DAC8 File Offset: 0x0004BCC8
		internal static Exception SqlResultSetClosed(string methodname)
		{
			if (methodname == null)
			{
				return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SqlResultSetClosed2, Array.Empty<object>()));
			}
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SqlResultSetClosed, new object[] { methodname }));
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0004DAFB File Offset: 0x0004BCFB
		internal static Exception SqlResultSetNoData(string methodname)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_DataReaderNoData, new object[] { methodname }));
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0004DB16 File Offset: 0x0004BD16
		internal static Exception SqlRecordReadOnly(string methodname)
		{
			if (methodname == null)
			{
				return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SqlRecordReadOnly2, Array.Empty<object>()));
			}
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SqlRecordReadOnly, new object[] { methodname }));
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0004DB49 File Offset: 0x0004BD49
		internal static Exception SqlResultSetRowDeleted(string methodname)
		{
			if (methodname == null)
			{
				return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SqlResultSetRowDeleted2, Array.Empty<object>()));
			}
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SqlResultSetRowDeleted, new object[] { methodname }));
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0004DB7C File Offset: 0x0004BD7C
		internal static Exception SqlResultSetCommandNotInSameConnection()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SqlResultSetCommandNotInSameConnection, Array.Empty<object>()));
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0004DB92 File Offset: 0x0004BD92
		internal static Exception SqlResultSetNoAcceptableCursor()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_SqlResultSetNoAcceptableCursor, Array.Empty<object>()));
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004DBA8 File Offset: 0x0004BDA8
		internal static Exception BulkLoadMappingInaccessible()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadMappingInaccessible, Array.Empty<object>()));
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0004DBBE File Offset: 0x0004BDBE
		internal static Exception BulkLoadMappingsNamesOrOrdinalsOnly()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadMappingsNamesOrOrdinalsOnly, Array.Empty<object>()));
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0004DBD4 File Offset: 0x0004BDD4
		internal static Exception BulkLoadCannotConvertValue(Type sourcetype, MetaType metatype, int ordinal, int rowNumber, bool isEncrypted, string columnName, string value, Exception e)
		{
			string text = string.Empty;
			if (!isEncrypted)
			{
				text = string.Format(" '{0}'", (value.Length > 100) ? value.Substring(0, 100) : value);
			}
			if (rowNumber == -1)
			{
				return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadCannotConvertValueWithoutRowNo, new object[] { text, sourcetype.Name, metatype.TypeName, ordinal, columnName }), e);
			}
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadCannotConvertValue, new object[] { text, sourcetype.Name, metatype.TypeName, ordinal, columnName, rowNumber }), e);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0004DC93 File Offset: 0x0004BE93
		internal static Exception BulkLoadNonMatchingColumnMapping()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadNonMatchingColumnMapping, Array.Empty<object>()));
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0004DCA9 File Offset: 0x0004BEA9
		internal static Exception BulkLoadNonMatchingColumnName(string columnName)
		{
			return SQL.BulkLoadNonMatchingColumnName(columnName, null);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0004DCB2 File Offset: 0x0004BEB2
		internal static Exception BulkLoadNonMatchingColumnName(string columnName, Exception e)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadNonMatchingColumnName, new object[] { columnName }), e);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0004DCCE File Offset: 0x0004BECE
		internal static Exception BulkLoadNullEmptyColumnName(string paramName)
		{
			return ADP.Argument(string.Format(StringsHelper.GetString(Strings.SQL_ParameterCannotBeEmpty, Array.Empty<object>()), paramName));
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0004DCEA File Offset: 0x0004BEEA
		internal static Exception BulkLoadUnspecifiedSortOrder()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_BulkLoadUnspecifiedSortOrder, Array.Empty<object>()));
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0004DD00 File Offset: 0x0004BF00
		internal static Exception BulkLoadInvalidOrderHint()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_BulkLoadInvalidOrderHint, Array.Empty<object>()));
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0004DD16 File Offset: 0x0004BF16
		internal static Exception BulkLoadOrderHintInvalidColumn(string columnName)
		{
			return ADP.InvalidOperation(string.Format(StringsHelper.GetString(Strings.SQL_BulkLoadOrderHintInvalidColumn, Array.Empty<object>()), columnName));
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0004DD32 File Offset: 0x0004BF32
		internal static Exception BulkLoadOrderHintDuplicateColumn(string columnName)
		{
			return ADP.InvalidOperation(string.Format(StringsHelper.GetString(Strings.SQL_BulkLoadOrderHintDuplicateColumn, Array.Empty<object>()), columnName));
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004DD4E File Offset: 0x0004BF4E
		internal static Exception BulkLoadStringTooLong(string tableName, string columnName, string truncatedValue)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadStringTooLong, new object[] { tableName, columnName, truncatedValue }));
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0004DD71 File Offset: 0x0004BF71
		internal static Exception BulkLoadInvalidVariantValue()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadInvalidVariantValue, Array.Empty<object>()));
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0004DD87 File Offset: 0x0004BF87
		internal static Exception BulkLoadInvalidTimeout(int timeout)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_BulkLoadInvalidTimeout, new object[] { timeout.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0004DDAD File Offset: 0x0004BFAD
		internal static Exception BulkLoadExistingTransaction()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadExistingTransaction, Array.Empty<object>()));
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004DDC3 File Offset: 0x0004BFC3
		internal static Exception BulkLoadNoCollation()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadNoCollation, Array.Empty<object>()));
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004DDD9 File Offset: 0x0004BFD9
		internal static Exception BulkLoadConflictingTransactionOption()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_BulkLoadConflictingTransactionOption, Array.Empty<object>()));
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004DDEF File Offset: 0x0004BFEF
		internal static Exception BulkLoadLcidMismatch(int sourceLcid, string sourceColumnName, int destinationLcid, string destinationColumnName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.Sql_BulkLoadLcidMismatch, new object[] { sourceLcid, sourceColumnName, destinationLcid, destinationColumnName }));
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004DE20 File Offset: 0x0004C020
		internal static Exception InvalidOperationInsideEvent()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadInvalidOperationInsideEvent, Array.Empty<object>()));
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004DE36 File Offset: 0x0004C036
		internal static Exception BulkLoadMissingDestinationTable()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadMissingDestinationTable, Array.Empty<object>()));
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004DE4C File Offset: 0x0004C04C
		internal static Exception BulkLoadInvalidDestinationTable(string tableName, Exception inner)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadInvalidDestinationTable, new object[] { tableName }), inner);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004DE68 File Offset: 0x0004C068
		internal static Exception BulkLoadBulkLoadNotAllowDBNull(string columnName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadNotAllowDBNull, new object[] { columnName }));
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004DE83 File Offset: 0x0004C083
		internal static Exception BulkLoadPendingOperation()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BulkLoadPendingOperation, Array.Empty<object>()));
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004DE9C File Offset: 0x0004C09C
		internal static Exception InvalidKeyEncryptionAlgorithm(string encryptionAlgorithm, string validEncryptionAlgorithm, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidKeyEncryptionAlgorithmSysErr, new object[] { encryptionAlgorithm, validEncryptionAlgorithm }), "encryptionAlgorithm");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidKeyEncryptionAlgorithm, new object[] { encryptionAlgorithm, validEncryptionAlgorithm }), "encryptionAlgorithm");
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0004DEF1 File Offset: 0x0004C0F1
		internal static Exception NullKeyEncryptionAlgorithm(bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.ArgumentNull("encryptionAlgorithm", StringsHelper.GetString(Strings.TCE_NullKeyEncryptionAlgorithmSysErr, Array.Empty<object>()));
			}
			return ADP.ArgumentNull("encryptionAlgorithm", StringsHelper.GetString(Strings.TCE_NullKeyEncryptionAlgorithm, Array.Empty<object>()));
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004DF29 File Offset: 0x0004C129
		internal static Exception EmptyColumnEncryptionKey()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyColumnEncryptionKey, Array.Empty<object>()), "columnEncryptionKey");
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004DF44 File Offset: 0x0004C144
		internal static Exception NullColumnEncryptionKey()
		{
			return ADP.ArgumentNull("columnEncryptionKey", StringsHelper.GetString(Strings.TCE_NullColumnEncryptionKey, Array.Empty<object>()));
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0004DF5F File Offset: 0x0004C15F
		internal static Exception EmptyEncryptedColumnEncryptionKey()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyEncryptedColumnEncryptionKey, Array.Empty<object>()), "encryptedColumnEncryptionKey");
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0004DF7A File Offset: 0x0004C17A
		internal static Exception NullEncryptedColumnEncryptionKey()
		{
			return ADP.ArgumentNull("encryptedColumnEncryptionKey", StringsHelper.GetString(Strings.TCE_NullEncryptedColumnEncryptionKey, Array.Empty<object>()));
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0004DF98 File Offset: 0x0004C198
		internal static Exception LargeCertificatePathLength(int actualLength, int maxLength, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_LargeCertificatePathLengthSysErr, new object[] { actualLength, maxLength }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_LargeCertificatePathLength, new object[] { actualLength, maxLength }), "masterKeyPath");
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0004E004 File Offset: 0x0004C204
		internal static Exception NullCertificatePath(string[] validLocations, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.ArgumentNull("masterKeyPath", StringsHelper.GetString(Strings.TCE_NullCertificatePathSysErr, new object[]
				{
					validLocations[0],
					validLocations[1],
					"/"
				}));
			}
			return ADP.ArgumentNull("masterKeyPath", StringsHelper.GetString(Strings.TCE_NullCertificatePath, new object[]
			{
				validLocations[0],
				validLocations[1],
				"/"
			}));
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0004E074 File Offset: 0x0004C274
		internal static Exception NullCspKeyPath(bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.ArgumentNull("masterKeyPath", StringsHelper.GetString(Strings.TCE_NullCspPathSysErr, new object[] { "/" }));
			}
			return ADP.ArgumentNull("masterKeyPath", StringsHelper.GetString(Strings.TCE_NullCspPath, new object[] { "/" }));
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0004E0CC File Offset: 0x0004C2CC
		internal static Exception NullCngKeyPath(bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.ArgumentNull("masterKeyPath", StringsHelper.GetString(Strings.TCE_NullCngPathSysErr, new object[] { "/" }));
			}
			return ADP.ArgumentNull("masterKeyPath", StringsHelper.GetString(Strings.TCE_NullCngPath, new object[] { "/" }));
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0004E124 File Offset: 0x0004C324
		internal static Exception InvalidCertificatePath(string actualCertificatePath, string[] validLocations, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCertificatePathSysErr, new object[]
				{
					actualCertificatePath,
					validLocations[0],
					validLocations[1],
					"/"
				}), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCertificatePath, new object[]
			{
				actualCertificatePath,
				validLocations[0],
				validLocations[1],
				"/"
			}), "masterKeyPath");
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0004E19C File Offset: 0x0004C39C
		internal static Exception InvalidCspPath(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCspPathSysErr, new object[] { masterKeyPath, "/" }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCspPath, new object[] { masterKeyPath, "/" }), "masterKeyPath");
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0004E1FC File Offset: 0x0004C3FC
		internal static Exception InvalidCngPath(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCngPathSysErr, new object[] { masterKeyPath, "/" }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCngPath, new object[] { masterKeyPath, "/" }), "masterKeyPath");
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0004E25C File Offset: 0x0004C45C
		internal static Exception EmptyCspName(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyCspNameSysErr, new object[] { masterKeyPath, "/" }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyCspName, new object[] { masterKeyPath, "/" }), "masterKeyPath");
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0004E2BC File Offset: 0x0004C4BC
		internal static Exception EmptyCngName(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyCngNameSysErr, new object[] { masterKeyPath, "/" }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyCngName, new object[] { masterKeyPath, "/" }), "masterKeyPath");
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0004E31C File Offset: 0x0004C51C
		internal static Exception EmptyCspKeyId(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyCspKeyIdSysErr, new object[] { masterKeyPath, "/" }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyCspKeyId, new object[] { masterKeyPath, "/" }), "masterKeyPath");
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0004E37C File Offset: 0x0004C57C
		internal static Exception EmptyCngKeyId(string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyCngKeyIdSysErr, new object[] { masterKeyPath, "/" }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyCngKeyId, new object[] { masterKeyPath, "/" }), "masterKeyPath");
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0004E3DC File Offset: 0x0004C5DC
		internal static Exception InvalidCspName(string cspName, string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCspNameSysErr, new object[] { cspName, masterKeyPath }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCspName, new object[] { cspName, masterKeyPath }), "masterKeyPath");
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0004E434 File Offset: 0x0004C634
		internal static Exception InvalidCspKeyIdentifier(string keyIdentifier, string masterKeyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCspKeyIdSysErr, new object[] { keyIdentifier, masterKeyPath }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCspKeyId, new object[] { keyIdentifier, masterKeyPath }), "masterKeyPath");
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0004E48C File Offset: 0x0004C68C
		internal static Exception InvalidCngKey(string masterKeyPath, string cngProviderName, string keyIdentifier, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCngKeySysErr, new object[] { masterKeyPath, cngProviderName, keyIdentifier }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCngKey, new object[] { masterKeyPath, cngProviderName, keyIdentifier }), "masterKeyPath");
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0004E4EC File Offset: 0x0004C6EC
		internal static Exception InvalidCertificateLocation(string certificateLocation, string certificatePath, string[] validLocations, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCertificateLocationSysErr, new object[]
				{
					certificateLocation,
					certificatePath,
					validLocations[0],
					validLocations[1],
					"/"
				}), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCertificateLocation, new object[]
			{
				certificateLocation,
				certificatePath,
				validLocations[0],
				validLocations[1],
				"/"
			}), "masterKeyPath");
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0004E56C File Offset: 0x0004C76C
		internal static Exception InvalidCertificateStore(string certificateStore, string certificatePath, string validCertificateStore, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCertificateStoreSysErr, new object[] { certificateStore, certificatePath, validCertificateStore }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCertificateStore, new object[] { certificateStore, certificatePath, validCertificateStore }), "masterKeyPath");
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0004E5CC File Offset: 0x0004C7CC
		internal static Exception EmptyCertificateThumbprint(string certificatePath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyCertificateThumbprintSysErr, new object[] { certificatePath }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyCertificateThumbprint, new object[] { certificatePath }), "masterKeyPath");
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0004E61C File Offset: 0x0004C81C
		internal static Exception CertificateNotFound(string thumbprint, string certificateLocation, string certificateStore, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_CertificateNotFoundSysErr, new object[] { thumbprint, certificateLocation, certificateStore }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_CertificateNotFound, new object[] { thumbprint, certificateLocation, certificateStore }), "masterKeyPath");
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0004E679 File Offset: 0x0004C879
		internal static Exception InvalidAlgorithmVersionInEncryptedCEK(byte actual, byte expected)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidAlgorithmVersionInEncryptedCEK, new object[]
			{
				actual.ToString("X2"),
				expected.ToString("X2")
			}), "encryptedColumnEncryptionKey");
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0004E6B3 File Offset: 0x0004C8B3
		internal static Exception InvalidCiphertextLengthInEncryptedCEK(int actual, int expected, string certificateName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCiphertextLengthInEncryptedCEK, new object[] { actual, expected, certificateName }), "encryptedColumnEncryptionKey");
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0004E6E5 File Offset: 0x0004C8E5
		internal static Exception InvalidCiphertextLengthInEncryptedCEKCsp(int actual, int expected, string masterKeyPath)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCiphertextLengthInEncryptedCEKCsp, new object[] { actual, expected, masterKeyPath }), "encryptedColumnEncryptionKey");
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004E717 File Offset: 0x0004C917
		internal static Exception InvalidCiphertextLengthInEncryptedCEKCng(int actual, int expected, string masterKeyPath)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCiphertextLengthInEncryptedCEKCng, new object[] { actual, expected, masterKeyPath }), "encryptedColumnEncryptionKey");
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0004E749 File Offset: 0x0004C949
		internal static Exception InvalidSignatureInEncryptedCEK(int actual, int expected, string masterKeyPath)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidSignatureInEncryptedCEK, new object[] { actual, expected, masterKeyPath }), "encryptedColumnEncryptionKey");
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0004E77B File Offset: 0x0004C97B
		internal static Exception InvalidSignatureInEncryptedCEKCsp(int actual, int expected, string masterKeyPath)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidSignatureInEncryptedCEKCsp, new object[] { actual, expected, masterKeyPath }), "encryptedColumnEncryptionKey");
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0004E7AD File Offset: 0x0004C9AD
		internal static Exception InvalidSignatureInEncryptedCEKCng(int actual, int expected, string masterKeyPath)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidSignatureInEncryptedCEKCng, new object[] { actual, expected, masterKeyPath }), "encryptedColumnEncryptionKey");
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0004E7DF File Offset: 0x0004C9DF
		internal static Exception InvalidCertificateSignature(string certificatePath)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCertificateSignature, new object[] { certificatePath }), "encryptedColumnEncryptionKey");
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0004E7FF File Offset: 0x0004C9FF
		internal static Exception InvalidSignature(string masterKeyPath)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidSignature, new object[] { masterKeyPath }), "encryptedColumnEncryptionKey");
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004E820 File Offset: 0x0004CA20
		internal static Exception CertificateWithNoPrivateKey(string keyPath, bool isSystemOp)
		{
			if (isSystemOp)
			{
				return ADP.Argument(StringsHelper.GetString(Strings.TCE_CertificateWithNoPrivateKeySysErr, new object[] { keyPath }), "masterKeyPath");
			}
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_CertificateWithNoPrivateKey, new object[] { keyPath }), "masterKeyPath");
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004E86D File Offset: 0x0004CA6D
		internal static Exception NullColumnEncryptionKeySysErr()
		{
			return ADP.ArgumentNull("encryptionKey", StringsHelper.GetString(Strings.TCE_NullColumnEncryptionKeySysErr, Array.Empty<object>()));
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0004E888 File Offset: 0x0004CA88
		internal static Exception InvalidKeySize(string algorithmName, int actualKeylength, int expectedLength)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidKeySize, new object[] { algorithmName, actualKeylength, expectedLength }), "encryptionKey");
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0004E8BC File Offset: 0x0004CABC
		internal static Exception InvalidEncryptionType(string algorithmName, SqlClientEncryptionType encryptionType, params SqlClientEncryptionType[] validEncryptionTypes)
		{
			string tce_InvalidEncryptionType = Strings.TCE_InvalidEncryptionType;
			object[] array = new object[3];
			array[0] = algorithmName;
			array[1] = encryptionType.ToString();
			array[2] = string.Join(", ", validEncryptionTypes.Select((SqlClientEncryptionType validEncryptionType) => "'" + validEncryptionType.ToString() + "'"));
			return ADP.Argument(StringsHelper.GetString(tce_InvalidEncryptionType, array), "encryptionType");
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0004E929 File Offset: 0x0004CB29
		internal static Exception NullPlainText()
		{
			return ADP.ArgumentNull(StringsHelper.GetString(Strings.TCE_NullPlainText, Array.Empty<object>()));
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0004E93F File Offset: 0x0004CB3F
		internal static Exception VeryLargeCiphertext(long cipherTextLength, long maxCipherTextSize, long plainTextLength)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_VeryLargeCiphertext, new object[] { cipherTextLength, maxCipherTextSize, plainTextLength }));
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0004E971 File Offset: 0x0004CB71
		internal static Exception NullCipherText()
		{
			return ADP.ArgumentNull(StringsHelper.GetString(Strings.TCE_NullCipherText, Array.Empty<object>()));
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0004E987 File Offset: 0x0004CB87
		internal static Exception InvalidCipherTextSize(int actualSize, int minimumSize)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCipherTextSize, new object[] { actualSize, minimumSize }), "cipherText");
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0004E9B5 File Offset: 0x0004CBB5
		internal static Exception InvalidAlgorithmVersion(byte actual, byte expected)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidAlgorithmVersion, new object[]
			{
				actual.ToString("X2"),
				expected.ToString("X2")
			}), "cipherText");
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0004E9EF File Offset: 0x0004CBEF
		internal static Exception InvalidAuthenticationTag()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidAuthenticationTag, Array.Empty<object>()), "cipherText");
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0004EA0A File Offset: 0x0004CC0A
		internal static Exception NullColumnEncryptionAlgorithm(string supportedAlgorithms)
		{
			return ADP.ArgumentNull("encryptionAlgorithm", StringsHelper.GetString(Strings.TCE_NullColumnEncryptionAlgorithm, new object[] { supportedAlgorithms }));
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0004EA2A File Offset: 0x0004CC2A
		internal static Exception UnexpectedDescribeParamFormatParameterMetadata()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_UnexpectedDescribeParamFormatParameterMetadata, new object[] { "sp_describe_parameter_encryption" }));
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0004EA49 File Offset: 0x0004CC49
		internal static Exception UnexpectedDescribeParamFormatAttestationInfo(string enclaveType)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_UnexpectedDescribeParamFormatAttestationInfo, new object[] { "sp_describe_parameter_encryption", enclaveType }));
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0004EA6C File Offset: 0x0004CC6C
		internal static Exception InvalidEncryptionKeyOrdinalEnclaveMetadata(int ordinal, int maxOrdinal)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_InvalidEncryptionKeyOrdinalEnclaveMetadata, new object[] { ordinal, maxOrdinal }));
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0004EA95 File Offset: 0x0004CC95
		internal static Exception InvalidEncryptionKeyOrdinalParameterMetadata(int ordinal, int maxOrdinal)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_InvalidEncryptionKeyOrdinalParameterMetadata, new object[] { ordinal, maxOrdinal }));
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0004EABE File Offset: 0x0004CCBE
		public static Exception MultipleRowsReturnedForAttestationInfo()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_MultipleRowsReturnedForAttestationInfo, new object[] { "sp_describe_parameter_encryption" }));
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0004EADD File Offset: 0x0004CCDD
		internal static Exception ParamEncryptionMetadataMissing(string paramName, string procedureName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_ParamEncryptionMetaDataMissing, new object[] { "sp_describe_parameter_encryption", paramName, procedureName }));
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0004EB04 File Offset: 0x0004CD04
		internal static Exception ParamInvalidForceColumnEncryptionSetting(string paramName, string procedureName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_ParamInvalidForceColumnEncryptionSetting, new object[] { "ForceColumnEncryption(true)", paramName, procedureName, "SqlParameter" }));
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0004EB33 File Offset: 0x0004CD33
		internal static Exception ParamUnExpectedEncryptionMetadata(string paramName, string procedureName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_ParamUnExpectedEncryptionMetadata, new object[] { paramName, procedureName, "ForceColumnEncryption(true)", "SqlParameter" }));
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0004EB62 File Offset: 0x0004CD62
		internal static Exception ProcEncryptionMetadataMissing(string procedureName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_ProcEncryptionMetaDataMissing, new object[] { "sp_describe_parameter_encryption", procedureName }));
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004EB88 File Offset: 0x0004CD88
		internal static Exception InvalidKeyStoreProviderName(string providerName, List<string> systemProviders, List<string> customProviders)
		{
			string text = string.Join(", ", systemProviders.Select((string provider) => "'" + provider + "'"));
			string text2 = string.Join(", ", customProviders.Select((string provider) => "'" + provider + "'"));
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidKeyStoreProviderName, new object[] { providerName, text, text2 }));
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0004EC16 File Offset: 0x0004CE16
		internal static Exception UnableToVerifyColumnMasterKeySignature(Exception innerException)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_UnableToVerifyColumnMasterKeySignature, new object[] { innerException.Message }), innerException);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004EC37 File Offset: 0x0004CE37
		internal static Exception ColumnMasterKeySignatureVerificationFailed(string cmkPath)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_ColumnMasterKeySignatureVerificationFailed, new object[] { cmkPath }));
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0004EC52 File Offset: 0x0004CE52
		internal static Exception ColumnMasterKeySignatureNotFound(string cmkPath)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_ColumnMasterKeySignatureNotFound, new object[] { cmkPath }));
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0004EC6D File Offset: 0x0004CE6D
		internal static Exception ExceptionWhenGeneratingEnclavePackage(Exception innerException)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_ExceptionWhenGeneratingEnclavePackage, new object[] { innerException.Message }), innerException);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0004EC8E File Offset: 0x0004CE8E
		internal static Exception FailedToEncryptRegisterRulesBytePackage(Exception innerException)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_FailedToEncryptRegisterRulesBytePackage, new object[] { innerException.Message }), innerException);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0004ECAF File Offset: 0x0004CEAF
		internal static Exception InvalidKeyIdUnableToCastToUnsignedShort(int keyId, Exception innerException)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidKeyIdUnableToCastToUnsignedShort, new object[] { keyId, innerException.Message }), innerException);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0004ECD9 File Offset: 0x0004CED9
		internal static Exception InvalidDatabaseIdUnableToCastToUnsignedInt(int databaseId, Exception innerException)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidDatabaseIdUnableToCastToUnsignedInt, new object[] { databaseId, innerException.Message }), innerException);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0004ED03 File Offset: 0x0004CF03
		internal static Exception InvalidAttestationParameterUnableToConvertToUnsignedInt(string variableName, int intValue, string enclaveType, Exception innerException)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidAttestationParameterUnableToConvertToUnsignedInt, new object[] { enclaveType, intValue, variableName, innerException.Message }), innerException);
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0004ED35 File Offset: 0x0004CF35
		internal static Exception OffsetOutOfBounds(string argument, string type, string method)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_OffsetOutOfBounds, new object[] { type, method }));
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0004ED54 File Offset: 0x0004CF54
		internal static Exception InsufficientBuffer(string argument, string type, string method)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InsufficientBuffer, new object[] { argument, type, method }));
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004ED77 File Offset: 0x0004CF77
		internal static Exception ColumnEncryptionKeysNotFound()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_ColumnEncryptionKeysNotFound, Array.Empty<object>()));
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004ED90 File Offset: 0x0004CF90
		internal static SqlException AttestationFailed(string errorMessage, Exception innerException = null)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 0, null, errorMessage, string.Empty, 0, null)
			}, string.Empty, Guid.Empty, innerException);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0004EDCB File Offset: 0x0004CFCB
		internal static Exception AttestationInfoNotReturnedFromSqlServer(string enclaveType, string enclaveAttestationUrl)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_AttestationInfoNotReturnedFromSQLServer, new object[] { enclaveType, enclaveAttestationUrl }));
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0004EDEA File Offset: 0x0004CFEA
		internal static Exception NullArgumentInConstructorInternal(string argumentName, string objectUnderConstruction)
		{
			return ADP.ArgumentNull(argumentName, StringsHelper.GetString(Strings.TCE_NullArgumentInConstructorInternal, new object[] { argumentName, objectUnderConstruction }));
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004EE0A File Offset: 0x0004D00A
		internal static Exception EmptyArgumentInConstructorInternal(string argumentName, string objectUnderConstruction)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyArgumentInConstructorInternal, new object[] { argumentName, objectUnderConstruction }));
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0004EE29 File Offset: 0x0004D029
		internal static Exception NullArgumentInternal(string argumentName, string type, string method)
		{
			return ADP.ArgumentNull(argumentName, StringsHelper.GetString(Strings.TCE_NullArgumentInternal, new object[] { argumentName, type, method }));
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0004EE4D File Offset: 0x0004D04D
		internal static Exception EmptyArgumentInternal(string argumentName, string type, string method)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_EmptyArgumentInternal, new object[] { argumentName, type, method }));
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0004EE70 File Offset: 0x0004D070
		internal static Exception CannotGetSqlColumnEncryptionEnclaveProviderConfig(Exception innerException)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_CannotGetSqlColumnEncryptionEnclaveProviderConfig, new object[] { innerException.Message }), innerException);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004EE91 File Offset: 0x0004D091
		internal static Exception CannotCreateSqlColumnEncryptionEnclaveProvider(string providerName, string type, Exception innerException)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_CannotCreateSqlColumnEncryptionEnclaveProvider, new object[] { providerName, type, innerException.Message }), innerException);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004EEBA File Offset: 0x0004D0BA
		internal static Exception SqlColumnEncryptionEnclaveProviderNameCannotBeEmpty()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_SqlColumnEncryptionEnclaveProviderNameCannotBeEmpty, Array.Empty<object>()));
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004EED0 File Offset: 0x0004D0D0
		internal static Exception NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe(string enclaveType)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe, new object[] { "sp_describe_parameter_encryption", enclaveType }));
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0004EEF3 File Offset: 0x0004D0F3
		internal static Exception NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage(string enclaveType)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage, new object[] { enclaveType }));
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0004EF0E File Offset: 0x0004D10E
		internal static Exception EnclaveTypeNullForEnclaveBasedQuery()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_EnclaveTypeNullForEnclaveBasedQuery, Array.Empty<object>()));
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0004EF24 File Offset: 0x0004D124
		internal static Exception EnclaveProvidersNotConfiguredForEnclaveBasedQuery()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_EnclaveProvidersNotConfiguredForEnclaveBasedQuery, Array.Empty<object>()));
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0004EF3A File Offset: 0x0004D13A
		internal static Exception EnclaveProviderNotFound(string enclaveType, string attestationProtocol)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_EnclaveProviderNotFound, new object[] { enclaveType, attestationProtocol }));
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0004EF59 File Offset: 0x0004D159
		internal static Exception AttestationProtocolNotSpecifiedForGeneratingEnclavePackage()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_AttestationProtocolNotSpecifiedForGeneratingEnclavePackage, Array.Empty<object>()));
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004EF6F File Offset: 0x0004D16F
		internal static Exception NullEnclaveSessionReturnedFromProvider(string enclaveType, string attestationUrl)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_NullEnclaveSessionReturnedFromProvider, new object[] { enclaveType, attestationUrl }));
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0004EF90 File Offset: 0x0004D190
		internal static Exception GetExceptionArray(string serverName, string errorMessage, Exception e)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
			Exception ex = ((e.InnerException != null) ? e.InnerException : e);
			sqlErrorCollection.Add(new SqlError(0, 0, 11, serverName, errorMessage, null, 0, null));
			if (e is SqlException)
			{
				SqlException ex2 = (SqlException)e;
				SqlErrorCollection errors = ex2.Errors;
				for (int i = 0; i < ex2.Errors.Count; i++)
				{
					sqlErrorCollection.Add(errors[i]);
				}
			}
			else
			{
				sqlErrorCollection.Add(new SqlError(0, 0, 11, serverName, e.Message, null, 0, null));
			}
			return SqlException.CreateException(sqlErrorCollection, "", null, ex);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0004F02F File Offset: 0x0004D22F
		internal static Exception ParamEncryptionFailed(string paramName, string serverName, Exception e)
		{
			return SQL.GetExceptionArray(serverName, StringsHelper.GetString(Strings.TCE_ParamEncryptionFailed, new object[] { paramName }), e);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0004F04C File Offset: 0x0004D24C
		internal static Exception ParamDecryptionFailed(string paramName, string serverName, Exception e)
		{
			return SQL.GetExceptionArray(serverName, StringsHelper.GetString(Strings.TCE_ParamDecryptionFailed, new object[] { paramName }), e);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0004F069 File Offset: 0x0004D269
		internal static Exception ColumnDecryptionFailed(string columnName, string serverName, Exception e)
		{
			return SQL.GetExceptionArray(serverName, StringsHelper.GetString(Strings.TCE_ColumnDecryptionFailed, new object[] { columnName }), e);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0004F086 File Offset: 0x0004D286
		internal static Exception UnknownColumnEncryptionAlgorithm(string algorithmName, string supportedAlgorithms)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_UnknownColumnEncryptionAlgorithm, new object[] { algorithmName, supportedAlgorithms }));
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0004F0A5 File Offset: 0x0004D2A5
		internal static Exception UnknownColumnEncryptionAlgorithmId(int algoId, string supportAlgorithmIds)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_UnknownColumnEncryptionAlgorithmId, new object[] { algoId, supportAlgorithmIds }), "cipherAlgorithmId");
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0004F0CE File Offset: 0x0004D2CE
		internal static Exception UnsupportedNormalizationVersion(byte version)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_UnsupportedNormalizationVersion, new object[] { version, "'1'", "SQL Server" }));
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0004F100 File Offset: 0x0004D300
		internal static Exception UnrecognizedKeyStoreProviderName(string providerName, List<string> systemProviders, List<string> customProviders)
		{
			string text = string.Join(", ", systemProviders.Select((string provider) => "'" + provider + "'"));
			string text2 = string.Join(", ", customProviders.Select((string provider) => "'" + provider + "'"));
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_UnrecognizedKeyStoreProviderName, new object[] { providerName, text, text2 }));
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0004F18E File Offset: 0x0004D38E
		internal static Exception InvalidDataTypeForEncryptedParameter(string parameterName, int actualDataType, int expectedDataType)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_NullProviderValue, new object[] { parameterName, actualDataType, expectedDataType }));
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0004F1BC File Offset: 0x0004D3BC
		internal static Exception KeyDecryptionFailed(string providerName, string keyHex, Exception e)
		{
			if (providerName.Equals("MSSQL_CERTIFICATE_STORE"))
			{
				return SQL.GetExceptionArray(null, StringsHelper.GetString(Strings.TCE_KeyDecryptionFailedCertStore, new object[] { providerName, keyHex }), e);
			}
			return SQL.GetExceptionArray(null, StringsHelper.GetString(Strings.TCE_KeyDecryptionFailed, new object[] { providerName, keyHex }), e);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0004F215 File Offset: 0x0004D415
		internal static Exception UntrustedKeyPath(string keyPath, string serverName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_UntrustedKeyPath, new object[] { keyPath, serverName }));
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0004F234 File Offset: 0x0004D434
		internal static Exception UnsupportedDatatypeEncryption(string dataType)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_UnsupportedDatatype, new object[] { dataType }));
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0004F24F File Offset: 0x0004D44F
		internal static Exception ThrowDecryptionFailed(string keyStr, string valStr, Exception e)
		{
			return SQL.GetExceptionArray(null, StringsHelper.GetString(Strings.TCE_DecryptionFailed, new object[] { keyStr, valStr }), e);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0004F270 File Offset: 0x0004D470
		internal static Exception NullEnclaveSessionDuringQueryExecution(string enclaveType, string enclaveAttestationUrl)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_NullEnclaveSessionDuringQueryExecution, new object[] { enclaveType, enclaveAttestationUrl }));
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0004F28F File Offset: 0x0004D48F
		internal static Exception NullEnclavePackageForEnclaveBasedQuery(string enclaveType, string enclaveAttestationUrl)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_NullEnclavePackageForEnclaveBasedQuery, new object[] { enclaveType, enclaveAttestationUrl }));
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0004F2AE File Offset: 0x0004D4AE
		internal static Exception TceNotSupported()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_NotSupportedByServer, new object[] { "SQL Server" }));
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0004F2CD File Offset: 0x0004D4CD
		internal static Exception EnclaveComputationsNotSupported()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_EnclaveComputationsNotSupported, Array.Empty<object>()));
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0004F2E3 File Offset: 0x0004D4E3
		internal static Exception AttestationURLNotSupported()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_AttestationURLNotSupported, Array.Empty<object>()));
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0004F2F9 File Offset: 0x0004D4F9
		internal static Exception AttestationProtocolNotSupported()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_AttestationProtocolNotSupported, Array.Empty<object>()));
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0004F30F File Offset: 0x0004D50F
		internal static Exception EnclaveTypeNotReturned()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_EnclaveTypeNotReturned, Array.Empty<object>()));
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0004F325 File Offset: 0x0004D525
		internal static Exception EnclaveTypeNotSupported(string enclaveType)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_EnclaveTypeNotSupported, new object[] { enclaveType }));
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0004F340 File Offset: 0x0004D540
		internal static Exception AttestationProtocolNotSupportEnclaveType(string attestationProtocolStr, string enclaveType)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_AttestationProtocolNotSupportEnclaveType, new object[] { attestationProtocolStr, enclaveType }));
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0004F35F File Offset: 0x0004D55F
		internal static Exception CanOnlyCallOnce()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.TCE_CanOnlyCallOnce, Array.Empty<object>()));
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0004F375 File Offset: 0x0004D575
		internal static Exception NullCustomKeyStoreProviderDictionary()
		{
			return ADP.ArgumentNull("clientKeyStoreProviders", StringsHelper.GetString(Strings.TCE_NullCustomKeyStoreProviderDictionary, Array.Empty<object>()));
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0004F390 File Offset: 0x0004D590
		internal static Exception InvalidCustomKeyStoreProviderName(string providerName, string prefix)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.TCE_InvalidCustomKeyStoreProviderName, new object[] { providerName, prefix }), "clientKeyStoreProviders");
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0004F3B4 File Offset: 0x0004D5B4
		internal static Exception NullProviderValue(string providerName)
		{
			return ADP.ArgumentNull("clientKeyStoreProviders", StringsHelper.GetString(Strings.TCE_NullProviderValue, new object[] { providerName }));
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0004F3D4 File Offset: 0x0004D5D4
		internal static Exception EmptyProviderName()
		{
			return ADP.ArgumentNull("clientKeyStoreProviders", StringsHelper.GetString(Strings.TCE_EmptyProviderName, Array.Empty<object>()));
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0004F3EF File Offset: 0x0004D5EF
		internal static Exception ConnectionDoomed()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ConnectionDoomed, Array.Empty<object>()));
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0004F405 File Offset: 0x0004D605
		internal static Exception OpenResultCountExceeded()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_OpenResultCountExceeded, Array.Empty<object>()));
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0004F41B File Offset: 0x0004D61B
		internal static Exception GlobalTransactionsNotEnabled()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.GT_Disabled, Array.Empty<object>()));
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0004F431 File Offset: 0x0004D631
		internal static Exception UnsupportedSysTxForGlobalTransactions()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.GT_UnsupportedSysTxVersion, Array.Empty<object>()));
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0004F448 File Offset: 0x0004D648
		internal static Exception MultiSubnetFailoverWithFailoverPartner(bool serverProvidedFailoverPartner, SqlInternalConnectionTds internalConnection)
		{
			string @string = StringsHelper.GetString(Strings.SQLMSF_FailoverPartnerNotSupported, Array.Empty<object>());
			if (serverProvidedFailoverPartner)
			{
				SqlException ex = SqlException.CreateException(new SqlErrorCollection
				{
					new SqlError(0, 0, 20, null, @string, "", 0, null)
				}, null, internalConnection, null);
				ex._doNotReconnect = true;
				return ex;
			}
			return ADP.Argument(@string);
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0004F4A0 File Offset: 0x0004D6A0
		internal static Exception MultiSubnetFailoverWithMoreThan64IPs()
		{
			string snierrorMessage = SQL.GetSNIErrorMessage(47);
			return ADP.InvalidOperation(snierrorMessage);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0004F4BC File Offset: 0x0004D6BC
		internal static Exception MultiSubnetFailoverWithInstanceSpecified()
		{
			string snierrorMessage = SQL.GetSNIErrorMessage(48);
			return ADP.Argument(snierrorMessage);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0004F4D8 File Offset: 0x0004D6D8
		internal static Exception MultiSubnetFailoverWithNonTcpProtocol()
		{
			string snierrorMessage = SQL.GetSNIErrorMessage(49);
			return ADP.Argument(snierrorMessage);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0004F4F3 File Offset: 0x0004D6F3
		internal static Exception ROR_FailoverNotSupportedConnString()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQLROR_FailoverNotSupported, Array.Empty<object>()));
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0004F50C File Offset: 0x0004D70C
		internal static Exception ROR_FailoverNotSupportedServer(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, StringsHelper.GetString(Strings.SQLROR_FailoverNotSupported, Array.Empty<object>()), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0004F558 File Offset: 0x0004D758
		internal static Exception ROR_RecursiveRoutingNotSupported(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, StringsHelper.GetString(Strings.SQLROR_RecursiveRoutingNotSupported, Array.Empty<object>()), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0004F5A4 File Offset: 0x0004D7A4
		internal static Exception ROR_UnexpectedRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, StringsHelper.GetString(Strings.SQLROR_UnexpectedRoutingInfo, Array.Empty<object>()), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0004F5F0 File Offset: 0x0004D7F0
		internal static Exception ROR_InvalidRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, StringsHelper.GetString(Strings.SQLROR_InvalidRoutingInfo, Array.Empty<object>()), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0004F63C File Offset: 0x0004D83C
		internal static Exception ROR_TimeoutAfterRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, StringsHelper.GetString(Strings.SQLROR_TimeoutAfterRoutingInfo, Array.Empty<object>()), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0004F688 File Offset: 0x0004D888
		internal static SqlException CR_ReconnectTimeout()
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(-2, 0, 11, null, SQLMessage.Timeout(), "", 0, 258U, null)
			}, "");
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0004F6CC File Offset: 0x0004D8CC
		internal static SqlException CR_ReconnectionCancelled()
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SQLMessage.OperationCancelled(), "", 0, null)
			}, "");
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0004F708 File Offset: 0x0004D908
		internal static Exception CR_NextAttemptWillExceedQueryTimeout(SqlException innerException, Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, StringsHelper.GetString(Strings.SQLCR_NextAttemptWillExceedQueryTimeout, Array.Empty<object>()), "", 0, null)
			}, "", connectionId, innerException);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0004F750 File Offset: 0x0004D950
		internal static Exception CR_EncryptionChanged(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, StringsHelper.GetString(Strings.SQLCR_EncryptionChanged, Array.Empty<object>()), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0004F798 File Offset: 0x0004D998
		internal static SqlException CR_AllAttemptsFailed(SqlException innerException, Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, StringsHelper.GetString(Strings.SQLCR_AllAttemptsFailed, Array.Empty<object>()), "", 0, null)
			}, "", connectionId, innerException);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0004F7E0 File Offset: 0x0004D9E0
		internal static SqlException CR_NoCRAckAtReconnection(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, StringsHelper.GetString(Strings.SQLCR_NoCRAckAtReconnection, Array.Empty<object>()), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0004F828 File Offset: 0x0004DA28
		internal static SqlException CR_TDSVersionNotPreserved(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, StringsHelper.GetString(Strings.SQLCR_TDSVestionNotPreserved, Array.Empty<object>()), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0004F870 File Offset: 0x0004DA70
		internal static SqlException CR_UnrecoverableServer(Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, StringsHelper.GetString(Strings.SQLCR_UnrecoverableServer, Array.Empty<object>()), "", 0, null)
			}, "", connectionId, null);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0004F8B8 File Offset: 0x0004DAB8
		internal static SqlException CR_UnrecoverableClient(Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, StringsHelper.GetString(Strings.SQLCR_UnrecoverableClient, Array.Empty<object>()), "", 0, null)
			}, "", connectionId, null);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0004F900 File Offset: 0x0004DB00
		internal static Exception Azure_ManagedIdentityException(string msg)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, msg, "", 0, null)
			};
			SqlException ex = SqlException.CreateException(sqlErrorCollection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0004F93B File Offset: 0x0004DB3B
		internal static Exception BatchedUpdatesNotAvailableOnContextConnection()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_BatchedUpdatesNotAvailableOnContextConnection, Array.Empty<object>()));
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0004F951 File Offset: 0x0004DB51
		internal static Exception ContextAllowsLimitedKeywords()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ContextAllowsLimitedKeywords, Array.Empty<object>()));
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0004F967 File Offset: 0x0004DB67
		internal static Exception ContextAllowsOnlyTypeSystem2005()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ContextAllowsOnlyTypeSystem2005, Array.Empty<object>()));
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0004F97D File Offset: 0x0004DB7D
		internal static Exception ContextConnectionIsInUse()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ContextConnectionIsInUse, Array.Empty<object>()));
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0004F993 File Offset: 0x0004DB93
		internal static Exception ContextUnavailableOutOfProc()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ContextUnavailableOutOfProc, Array.Empty<object>()));
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0004F9A9 File Offset: 0x0004DBA9
		internal static Exception ContextUnavailableWhileInProc()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_ContextUnavailableWhileInProc, Array.Empty<object>()));
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0004F9BF File Offset: 0x0004DBBF
		internal static Exception NestedTransactionScopesNotSupported()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_NestedTransactionScopesNotSupported, Array.Empty<object>()));
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0004F9D5 File Offset: 0x0004DBD5
		internal static Exception NotAvailableOnContextConnection()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_NotAvailableOnContextConnection, Array.Empty<object>()));
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0004F9EB File Offset: 0x0004DBEB
		internal static Exception NotificationsNotAvailableOnContextConnection()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_NotificationsNotAvailableOnContextConnection, Array.Empty<object>()));
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0004FA01 File Offset: 0x0004DC01
		internal static Exception UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType eventType)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_UnexpectedSmiEvent, new object[] { (int)eventType }));
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0004FA21 File Offset: 0x0004DC21
		internal static Exception UserInstanceNotAvailableInProc()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_UserInstanceNotAvailableInProc, Array.Empty<object>()));
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0004FA37 File Offset: 0x0004DC37
		internal static Exception ArgumentLengthMismatch(string arg1, string arg2)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_ArgumentLengthMismatch, new object[] { arg1, arg2 }));
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0004FA56 File Offset: 0x0004DC56
		internal static Exception InvalidSqlDbTypeOneAllowedType(SqlDbType invalidType, string method, SqlDbType allowedType)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_InvalidSqlDbTypeWithOneAllowedType, new object[] { invalidType, method, allowedType }));
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0004FA83 File Offset: 0x0004DC83
		internal static Exception SqlPipeErrorRequiresSendEnd()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SQL_PipeErrorRequiresSendEnd, Array.Empty<object>()));
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0004FA99 File Offset: 0x0004DC99
		internal static Exception TooManyValues(string arg)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQL_TooManyValues, Array.Empty<object>()), arg);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0004FAB0 File Offset: 0x0004DCB0
		internal static Exception StreamWriteNotSupported()
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SQL_StreamWriteNotSupported, Array.Empty<object>()));
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0004FAC6 File Offset: 0x0004DCC6
		internal static Exception StreamReadNotSupported()
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SQL_StreamReadNotSupported, Array.Empty<object>()));
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0004FADC File Offset: 0x0004DCDC
		internal static Exception StreamSeekNotSupported()
		{
			return ADP.NotSupported(StringsHelper.GetString(Strings.SQL_StreamSeekNotSupported, Array.Empty<object>()));
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0004FAF4 File Offset: 0x0004DCF4
		internal static SqlNullValueException SqlNullValue()
		{
			SqlNullValueException ex = new SqlNullValueException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0004FB0E File Offset: 0x0004DD0E
		internal static Exception ParameterSizeRestrictionFailure(int index)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.OleDb_CommandParameterError, new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				"SqlParameter.Size"
			}));
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0004FB3C File Offset: 0x0004DD3C
		internal static Exception SubclassMustOverride()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.SqlMisc_SubclassMustOverride, Array.Empty<object>()));
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0004FB54 File Offset: 0x0004DD54
		internal static string GetSNIErrorMessage(int sniError)
		{
			string text = string.Format("SNI_ERROR_{0}", sniError);
			return StringsHelper.GetString(text, Array.Empty<object>());
		}

		// Token: 0x040007E1 RID: 2017
		internal static readonly byte[] AttentionHeader = new byte[] { 6, 1, 0, 8, 0, 0, 0, 0 };

		// Token: 0x040007E2 RID: 2018
		internal const string WriteToServer = "WriteToServer";

		// Token: 0x040007E3 RID: 2019
		internal const int SqlDependencyTimeoutDefault = 0;

		// Token: 0x040007E4 RID: 2020
		internal const int SqlDependencyServerTimeout = 432000;

		// Token: 0x040007E5 RID: 2021
		internal const string SqlNotificationServiceDefault = "SqlQueryNotificationService";

		// Token: 0x040007E6 RID: 2022
		internal const string SqlNotificationStoredProcedureDefault = "SqlQueryNotificationStoredProcedure";

		// Token: 0x040007E7 RID: 2023
		internal const string Transaction = "Transaction";

		// Token: 0x040007E8 RID: 2024
		internal const string Connection = "Connection";
	}
}
