using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001EE RID: 494
	internal interface INamedMetadataObjectBody : IMetadataObjectBody, ITxObjectBody
	{
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001C8D RID: 7309
		// (set) Token: 0x06001C8E RID: 7310
		bool RenameRequestedThroughAPI { get; set; }

		// Token: 0x06001C8F RID: 7311
		string GetObjectName();
	}
}
