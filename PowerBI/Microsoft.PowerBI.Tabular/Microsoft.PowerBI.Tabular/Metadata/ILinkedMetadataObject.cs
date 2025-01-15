using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001E8 RID: 488
	internal interface ILinkedMetadataObject
	{
		// Token: 0x06001C6F RID: 7279
		void GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property);
	}
}
