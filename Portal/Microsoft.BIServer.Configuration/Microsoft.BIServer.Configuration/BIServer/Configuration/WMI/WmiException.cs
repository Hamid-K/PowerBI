using System;
using System.Runtime.InteropServices;

namespace Microsoft.BIServer.Configuration.WMI
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	internal sealed class WmiException : Exception
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00006953 File Offset: 0x00004B53
		public WmiException()
			: base(null)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006963 File Offset: 0x00004B63
		public WmiException(string message)
			: base(message)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006973 File Offset: 0x00004B73
		public WmiException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006984 File Offset: 0x00004B84
		public WmiException(ErrorCodes errorCode)
			: base(WmiException.ErrorCodeToMessage(errorCode))
		{
			this.m_errorCode = errorCode;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000069A0 File Offset: 0x00004BA0
		public WmiException(ErrorCodes errorCode, Exception innerException)
			: base(WmiException.ErrorCodeToMessage(errorCode), innerException)
		{
			this.m_errorCode = errorCode;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000069BD File Offset: 0x00004BBD
		private static string ErrorCodeToMessage(ErrorCodes errorCode)
		{
			return errorCode.ToString();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000069CC File Offset: 0x00004BCC
		internal static WmiException GetWmiExceptionForHR(int hresult)
		{
			return new WmiException((ErrorCodes)hresult, Marshal.GetExceptionForHR(hresult));
		}

		// Token: 0x04000183 RID: 387
		private ErrorCodes m_errorCode = (ErrorCodes)4294967295U;
	}
}
