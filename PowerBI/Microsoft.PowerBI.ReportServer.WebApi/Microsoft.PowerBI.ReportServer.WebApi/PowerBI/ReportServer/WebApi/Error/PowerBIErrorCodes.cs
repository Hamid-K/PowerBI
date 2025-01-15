using System;

namespace Microsoft.PowerBI.ReportServer.WebApi.Error
{
	// Token: 0x0200003A RID: 58
	public class PowerBIErrorCodes
	{
		// Token: 0x040000A7 RID: 167
		public const string ASConnectionError = "OnPremises_Connection_Error_General";

		// Token: 0x040000A8 RID: 168
		public const string ASConnection_Details_Title = "OnPremises_Connection_Error_Title";

		// Token: 0x040000A9 RID: 169
		public const string ASConnection_Details_ConnectionStringError = "OnPremises_Connection_Error_Connection_String";

		// Token: 0x040000AA RID: 170
		public const string ASConnection_Details_ClosedByHost = "OnPremises_Connection_Error_Connection_Closed";

		// Token: 0x040000AB RID: 171
		public const string ASConnection_Details_ImpersonationError = "OnPremises_Impersonation_Error";

		// Token: 0x040000AC RID: 172
		public const string ASConnection_Details_DbCredentialsError = "OnPremises_Connection_Error_AS_DbCredentials";

		// Token: 0x040000AD RID: 173
		public const string ExploreHost_CannotRetrieveModelError = "CannotRetrieveModelError";
	}
}
