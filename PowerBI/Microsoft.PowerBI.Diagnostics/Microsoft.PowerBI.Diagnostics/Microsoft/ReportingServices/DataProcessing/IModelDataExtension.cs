using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000030 RID: 48
	internal interface IModelDataExtension
	{
		// Token: 0x060000BB RID: 187
		string GetModelMetadata(string perspectiveName, string supportedVersion);

		// Token: 0x060000BC RID: 188
		void CancelModelMetadataRetrieval();
	}
}
