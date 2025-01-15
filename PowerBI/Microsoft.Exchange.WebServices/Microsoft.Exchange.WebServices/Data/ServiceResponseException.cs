using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000260 RID: 608
	[Serializable]
	public class ServiceResponseException : ServiceRemoteException
	{
		// Token: 0x060015B4 RID: 5556 RVA: 0x0003CDB8 File Offset: 0x0003BDB8
		internal ServiceResponseException(ServiceResponse response)
		{
			this.response = response;
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x0003CDC7 File Offset: 0x0003BDC7
		public ServiceResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0003CDCF File Offset: 0x0003BDCF
		public ServiceError ErrorCode
		{
			get
			{
				return this.response.ErrorCode;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x0003CDDC File Offset: 0x0003BDDC
		public override string Message
		{
			get
			{
				string text;
				string text2;
				string text3;
				if (this.Response.ErrorCode == ServiceError.ErrorInternalServerError && this.Response.ErrorDetails.TryGetValue("ExceptionClass", ref text) && this.Response.ErrorDetails.TryGetValue("ExceptionMessage", ref text2) && this.Response.ErrorDetails.TryGetValue("StackTrace", ref text3))
				{
					return string.Format(Strings.ServerErrorAndStackTraceDetails, new object[]
					{
						this.Response.ErrorMessage,
						text,
						text2,
						text3
					});
				}
				return this.Response.ErrorMessage;
			}
		}

		// Token: 0x040012B4 RID: 4788
		private const string ExceptionClassKey = "ExceptionClass";

		// Token: 0x040012B5 RID: 4789
		private const string ExceptionMessageKey = "ExceptionMessage";

		// Token: 0x040012B6 RID: 4790
		private const string StackTraceKey = "StackTrace";

		// Token: 0x040012B7 RID: 4791
		private ServiceResponse response;
	}
}
