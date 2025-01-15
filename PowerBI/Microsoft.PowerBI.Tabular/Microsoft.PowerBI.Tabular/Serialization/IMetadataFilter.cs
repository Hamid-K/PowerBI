using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000171 RID: 369
	internal interface IMetadataFilter
	{
		// Token: 0x0600176D RID: 5997
		bool IgnoreProperty(ObjectType owner, string propertyName, MetadataPropertyNature propertyNature);

		// Token: 0x0600176E RID: 5998
		bool IgnoreChild(ObjectType owner, string propertyName, MetadataPropertyNature propertyNature, MetadataObject @object);
	}
}
