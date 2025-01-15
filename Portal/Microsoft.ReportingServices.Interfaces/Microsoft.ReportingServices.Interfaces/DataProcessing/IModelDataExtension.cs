using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200001E RID: 30
	internal interface IModelDataExtension
	{
		// Token: 0x06000045 RID: 69
		string GetModelMetadata(string perspectiveName, string supportedVersion);

		// Token: 0x06000046 RID: 70
		void CancelModelMetadataRetrieval();
	}
}
