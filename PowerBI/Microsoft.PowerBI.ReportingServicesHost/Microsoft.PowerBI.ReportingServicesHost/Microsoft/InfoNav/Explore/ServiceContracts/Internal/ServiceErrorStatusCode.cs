using System;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x0200000E RID: 14
	public enum ServiceErrorStatusCode
	{
		// Token: 0x0400003A RID: 58
		GeneralError,
		// Token: 0x0400003B RID: 59
		CsdlFetching,
		// Token: 0x0400003C RID: 60
		CsdlConvertXmlToConceptualSchema,
		// Token: 0x0400003D RID: 61
		CsdlCreateClientSchema,
		// Token: 0x0400003E RID: 62
		ExecuteSemanticQueryError,
		// Token: 0x0400003F RID: 63
		ExecuteSemanticQueryInvalidStreamFormat,
		// Token: 0x04000040 RID: 64
		ExecuteSemanticQueryTransformError,
		// Token: 0x04000041 RID: 65
		TranslateSemanticQueryInvalidStreamFormat,
		// Token: 0x04000042 RID: 66
		TranslateSemanticQueryError,
		// Token: 0x04000043 RID: 67
		GetSchemaDataSetError,
		// Token: 0x04000044 RID: 68
		ExecuteDaxQueryError,
		// Token: 0x04000045 RID: 69
		ExecuteDaxQueryInvalidStreamFormat,
		// Token: 0x04000046 RID: 70
		InternalError
	}
}
