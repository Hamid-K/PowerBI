using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004BE RID: 1214
	internal static class ErrorHandlerHelper
	{
		// Token: 0x0600251E RID: 9502 RVA: 0x00084104 File Offset: 0x00082304
		public static FaultCode GetFaultCode(Exception error)
		{
			FaultException ex = error as FaultException;
			if (ex != null && ex.Code != null)
			{
				return ex.Code;
			}
			return new FaultCode("Sender");
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x00084134 File Offset: 0x00082334
		public static bool IsVersioningException(Exception exception, MessageVersion messageVersion)
		{
			if (exception is FaultException)
			{
				FaultCode faultCode = ErrorHandlerHelper.GetFaultCode(exception);
				FaultCode faultCode2;
				if (messageVersion.Envelope == EnvelopeVersion.Soap11)
				{
					faultCode2 = faultCode;
				}
				else
				{
					faultCode2 = faultCode.SubCode;
				}
				if (faultCode2 != null && faultCode2.Name.Equals("ActionNotSupported", StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}
	}
}
