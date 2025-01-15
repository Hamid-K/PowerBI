using System;

namespace Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform
{
	// Token: 0x0200001F RID: 31
	public interface IDaxDataTransformMetadataFactory
	{
		// Token: 0x06000051 RID: 81
		IDaxDataTransformMetadata Create(string name);

		// Token: 0x06000052 RID: 82
		bool HasTransform(string name);
	}
}
