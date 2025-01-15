using System;
using System.Net;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;

namespace Microsoft.ReportingServices
{
	// Token: 0x0200007E RID: 126
	public class PortalException : Exception
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x000048D6 File Offset: 0x00002AD6
		public PortalException(string message, HttpStatusCode statusCode, ErrorCode errorCode)
			: base(message)
		{
			this._statusCode = statusCode;
			this._errorCode = errorCode;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x000048ED File Offset: 0x00002AED
		public HttpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000048F5 File Offset: 0x00002AF5
		public ErrorCode ErrCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x040002BC RID: 700
		private readonly HttpStatusCode _statusCode;

		// Token: 0x040002BD RID: 701
		private readonly ErrorCode _errorCode;
	}
}
