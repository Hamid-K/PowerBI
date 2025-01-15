using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x0200000B RID: 11
	[DataContract]
	public sealed class ConceptualSchemaInfo
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000221D File Offset: 0x0000041D
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002225 File Offset: 0x00000425
		[DataMember(IsRequired = true, Order = 0, Name = "modelId")]
		public long ModelId { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000222E File Offset: 0x0000042E
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002236 File Offset: 0x00000436
		[DataMember(IsRequired = false, Order = 1, Name = "schema")]
		public ClientConceptualSchema Schema { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000223F File Offset: 0x0000043F
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002247 File Offset: 0x00000447
		[DataMember(IsRequired = false, Order = 20, Name = "error")]
		public ServiceError Error { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002250 File Offset: 0x00000450
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002258 File Offset: 0x00000458
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 21, Name = "cubeName")]
		public string CubeName { get; set; }

		// Token: 0x06000024 RID: 36 RVA: 0x00002264 File Offset: 0x00000464
		public static ConceptualSchemaInfo CreateFromException(Exception e, ServiceErrorExtractor extractor, ServiceErrorStatusCode statusCode, long modelId, string cubeName = null)
		{
			ServiceError serviceError = ConceptualSchemaInfo.ExtractServiceErrorAndSetStatusCode(e, extractor, statusCode);
			return new ConceptualSchemaInfo
			{
				ModelId = modelId,
				Error = serviceError,
				CubeName = (string.IsNullOrEmpty(cubeName) ? null : cubeName)
			};
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022A4 File Offset: 0x000004A4
		private static ServiceError ExtractServiceErrorAndSetStatusCode(Exception e, ServiceErrorExtractor extractor, ServiceErrorStatusCode statusCode)
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

		// Token: 0x04000033 RID: 51
		private const string RsCannotRetrieveModel = "rsCannotRetrieveModel";
	}
}
