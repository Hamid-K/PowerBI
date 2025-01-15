using System;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000013 RID: 19
	public interface IMetadataTransform
	{
		// Token: 0x0600002F RID: 47
		SchemaTransformResult GetSchema(SchemaTransformContext context);
	}
}
