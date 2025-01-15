using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x0200000E RID: 14
	[DataContract]
	public sealed class PerspectivesInfoResponse
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000241A File Offset: 0x0000061A
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002422 File Offset: 0x00000622
		[DataMember(IsRequired = false, Order = 0, Name = "perspectivesInfo")]
		public PerspectivesInfo PerspectivesInfo { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000242B File Offset: 0x0000062B
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002433 File Offset: 0x00000633
		[DataMember(IsRequired = false, Order = 1, Name = "error")]
		public ServiceError Error { get; set; }

		// Token: 0x06000038 RID: 56 RVA: 0x0000243C File Offset: 0x0000063C
		private static ServiceError CreateServiceErrorFromException(Exception e, ServiceErrorExtractor extractor, ServiceErrorStatusCode statusCode)
		{
			ServiceError serviceError;
			if (e != null && extractor.TryExtractServiceError(e, out serviceError))
			{
				serviceError.StatusCode = statusCode;
			}
			else
			{
				serviceError = new ServiceError
				{
					StatusCode = statusCode
				};
			}
			return serviceError;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002470 File Offset: 0x00000670
		public static PerspectivesInfoResponse CreatePerspectivesInfoResponseFromException(Exception e, ServiceErrorStatusCode statusCode, ServiceErrorExtractor extractor, string powerBIErrorCode = null)
		{
			ServiceError serviceError = PerspectivesInfoResponse.CreateServiceErrorFromException(e, extractor, statusCode);
			if (powerBIErrorCode != null)
			{
				serviceError.PowerBIErrorCode = powerBIErrorCode;
			}
			PerspectivesInfoResponse perspectivesInfoResponse = new PerspectivesInfoResponse
			{
				Error = serviceError
			};
			if (powerBIErrorCode != null)
			{
				perspectivesInfoResponse.Error.PowerBIErrorCode = powerBIErrorCode;
			}
			return perspectivesInfoResponse;
		}
	}
}
