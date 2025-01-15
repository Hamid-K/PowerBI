using System;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000011 RID: 17
	public interface IDataTransformPlugin
	{
		// Token: 0x0600002B RID: 43
		IDataTransform CreateDataTransform();

		// Token: 0x0600002C RID: 44
		IMetadataTransform CreateMetadataTransform();
	}
}
