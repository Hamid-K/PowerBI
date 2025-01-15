using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000F7 RID: 247
	internal sealed class SQLMessage
	{
		// Token: 0x0600143B RID: 5179 RVA: 0x000027D1 File Offset: 0x000009D1
		private SQLMessage()
		{
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0004FB95 File Offset: 0x0004DD95
		internal static string CultureIdError()
		{
			return StringsHelper.GetString(Strings.SQL_CultureIdError, Array.Empty<object>());
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0004FBA6 File Offset: 0x0004DDA6
		internal static string EncryptionNotSupportedByClient()
		{
			return StringsHelper.GetString(Strings.SQL_EncryptionNotSupportedByClient, Array.Empty<object>());
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0004FBB7 File Offset: 0x0004DDB7
		internal static string EncryptionNotSupportedByServer()
		{
			return StringsHelper.GetString(Strings.SQL_EncryptionNotSupportedByServer, Array.Empty<object>());
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0004FBC8 File Offset: 0x0004DDC8
		internal static string CTAIPNotSupportedByServer()
		{
			return StringsHelper.GetString(Strings.SQL_CTAIPNotSupportedByServer, Array.Empty<object>());
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0004FBD9 File Offset: 0x0004DDD9
		internal static string OperationCancelled()
		{
			return StringsHelper.GetString(Strings.SQL_OperationCancelled, Array.Empty<object>());
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0004FBEA File Offset: 0x0004DDEA
		internal static string SevereError()
		{
			return StringsHelper.GetString(Strings.SQL_SevereError, Array.Empty<object>());
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0004FBFB File Offset: 0x0004DDFB
		internal static string SSPIInitializeError()
		{
			return StringsHelper.GetString(Strings.SQL_SSPIInitializeError, Array.Empty<object>());
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0004FC0C File Offset: 0x0004DE0C
		internal static string SSPIGenerateError()
		{
			return StringsHelper.GetString(Strings.SQL_SSPIGenerateError, Array.Empty<object>());
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0004FC1D File Offset: 0x0004DE1D
		internal static string Timeout()
		{
			return StringsHelper.GetString(Strings.SQL_Timeout_Execution, Array.Empty<object>());
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0004FC2E File Offset: 0x0004DE2E
		internal static string Timeout_PreLogin_Begin()
		{
			return StringsHelper.GetString(Strings.SQL_Timeout_PreLogin_Begin, Array.Empty<object>());
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0004FC3F File Offset: 0x0004DE3F
		internal static string Timeout_PreLogin_InitializeConnection()
		{
			return StringsHelper.GetString(Strings.SQL_Timeout_PreLogin_InitializeConnection, Array.Empty<object>());
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0004FC50 File Offset: 0x0004DE50
		internal static string Timeout_PreLogin_SendHandshake()
		{
			return StringsHelper.GetString(Strings.SQL_Timeout_PreLogin_SendHandshake, Array.Empty<object>());
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0004FC61 File Offset: 0x0004DE61
		internal static string Timeout_PreLogin_ConsumeHandshake()
		{
			return StringsHelper.GetString(Strings.SQL_Timeout_PreLogin_ConsumeHandshake, Array.Empty<object>());
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0004FC72 File Offset: 0x0004DE72
		internal static string Timeout_Login_Begin()
		{
			return StringsHelper.GetString(Strings.SQL_Timeout_Login_Begin, Array.Empty<object>());
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0004FC83 File Offset: 0x0004DE83
		internal static string Timeout_Login_ProcessConnectionAuth()
		{
			return StringsHelper.GetString(Strings.SQL_Timeout_Login_ProcessConnectionAuth, Array.Empty<object>());
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0004FC94 File Offset: 0x0004DE94
		internal static string Timeout_PostLogin()
		{
			return StringsHelper.GetString(Strings.SQL_Timeout_PostLogin, Array.Empty<object>());
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0004FCA5 File Offset: 0x0004DEA5
		internal static string Timeout_FailoverInfo()
		{
			return StringsHelper.GetString(Strings.SQL_Timeout_FailoverInfo, Array.Empty<object>());
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0004FCB6 File Offset: 0x0004DEB6
		internal static string Timeout_RoutingDestination()
		{
			return StringsHelper.GetString(Strings.SQL_Timeout_RoutingDestinationInfo, Array.Empty<object>());
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0004FCC7 File Offset: 0x0004DEC7
		internal static string Duration_PreLogin_Begin(long PreLoginBeginDuration)
		{
			return StringsHelper.GetString(Strings.SQL_Duration_PreLogin_Begin, new object[] { PreLoginBeginDuration });
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0004FCE2 File Offset: 0x0004DEE2
		internal static string Duration_PreLoginHandshake(long PreLoginBeginDuration, long PreLoginHandshakeDuration)
		{
			return StringsHelper.GetString(Strings.SQL_Duration_PreLoginHandshake, new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration });
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0004FD06 File Offset: 0x0004DF06
		internal static string Duration_Login_Begin(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration)
		{
			return StringsHelper.GetString(Strings.SQL_Duration_Login_Begin, new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration, LoginBeginDuration });
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0004FD33 File Offset: 0x0004DF33
		internal static string Duration_Login_ProcessConnectionAuth(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration, long LoginAuthDuration)
		{
			return StringsHelper.GetString(Strings.SQL_Duration_Login_ProcessConnectionAuth, new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration, LoginBeginDuration, LoginAuthDuration });
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0004FD69 File Offset: 0x0004DF69
		internal static string Duration_PostLogin(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration, long LoginAuthDuration, long PostLoginDuration)
		{
			return StringsHelper.GetString(Strings.SQL_Duration_PostLogin, new object[] { PreLoginBeginDuration, PreLoginHandshakeDuration, LoginBeginDuration, LoginAuthDuration, PostLoginDuration });
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004FDA9 File Offset: 0x0004DFA9
		internal static string UserInstanceFailure()
		{
			return StringsHelper.GetString(Strings.SQL_UserInstanceFailure, Array.Empty<object>());
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004FDBA File Offset: 0x0004DFBA
		internal static string PreloginError()
		{
			return StringsHelper.GetString(Strings.Snix_PreLogin, Array.Empty<object>());
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0004FDCB File Offset: 0x0004DFCB
		internal static string ExClientConnectionId()
		{
			return StringsHelper.GetString(Strings.SQL_ExClientConnectionId, Array.Empty<object>());
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0004FDDC File Offset: 0x0004DFDC
		internal static string ExErrorNumberStateClass()
		{
			return StringsHelper.GetString(Strings.SQL_ExErrorNumberStateClass, Array.Empty<object>());
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0004FDED File Offset: 0x0004DFED
		internal static string ExOriginalClientConnectionId()
		{
			return StringsHelper.GetString(Strings.SQL_ExOriginalClientConnectionId, Array.Empty<object>());
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0004FDFE File Offset: 0x0004DFFE
		internal static string ExRoutingDestination()
		{
			return StringsHelper.GetString(Strings.SQL_ExRoutingDestination, Array.Empty<object>());
		}
	}
}
