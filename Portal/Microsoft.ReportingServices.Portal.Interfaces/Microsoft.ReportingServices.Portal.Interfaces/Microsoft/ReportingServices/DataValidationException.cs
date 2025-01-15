using System;
using System.Net;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;

namespace Microsoft.ReportingServices
{
	// Token: 0x0200007F RID: 127
	public class DataValidationException : PortalException
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x000048FD File Offset: 0x00002AFD
		public DataValidationException(string message)
			: base(message, HttpStatusCode.BadRequest, ErrorCode.InvalidContent)
		{
		}
	}
}
