using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000259 RID: 601
	[Serializable]
	public abstract class BatchServiceResponseException<TResponse> : ServiceRemoteException where TResponse : ServiceResponse
	{
		// Token: 0x060015A5 RID: 5541 RVA: 0x0003CCCB File Offset: 0x0003BCCB
		internal BatchServiceResponseException(ServiceResponseCollection<TResponse> serviceResponses, string message)
			: base(message)
		{
			EwsUtilities.Assert(serviceResponses != null, "MultiServiceResponseException.ctor", "serviceResponses is null");
			this.responses = serviceResponses;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0003CCF1 File Offset: 0x0003BCF1
		internal BatchServiceResponseException(ServiceResponseCollection<TResponse> serviceResponses, string message, Exception innerException)
			: base(message, innerException)
		{
			EwsUtilities.Assert(serviceResponses != null, "MultiServiceResponseException.ctor", "serviceResponses is null");
			this.responses = serviceResponses;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0003CD18 File Offset: 0x0003BD18
		public ServiceResponseCollection<TResponse> ServiceResponses
		{
			get
			{
				return this.responses;
			}
		}

		// Token: 0x040012B2 RID: 4786
		private ServiceResponseCollection<TResponse> responses;
	}
}
